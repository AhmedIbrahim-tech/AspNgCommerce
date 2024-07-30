using Azure.Core;
using ECommerce.Core.Entities.OrderAggregate;
using ECommerce.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace ECommerce.API.Controllers;

[ApiController]
public class PaymentsController : BaseAPIController
{

    #region Constractor (s)
    private readonly IPaymentServices _paymentService;
    private const string WhSecret = "whsec_6270bb0ddaa11d53f86f1c9efe45fbec393a98f40f945de9fcd8350be31409dc";
    private readonly ILogger<PaymentsController> _logger;

    public PaymentsController(IPaymentServices paymentService, ILogger<PaymentsController> logger)
    {
        _paymentService = paymentService;
        _logger = logger;
    }
    #endregion

    #region Create Or Update PaymentIntent
    [Authorize]
    [HttpPost("{basketId}")]
    public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
    {
        var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

        if (basket == null) return BadRequest(new BaseGenericResult<dynamic>(400, "Problem with your basket"));

        return basket;
    }

    #endregion

    #region Stripe Webhook
    [HttpPost("webhook")]
    public async Task<ActionResult> StripeWebhook()
    {
        var json = await new StreamReader(Request.Body).ReadToEndAsync();

        var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], WhSecret);

        PaymentIntent intent;
        Order order;

        switch (stripeEvent.Type)
        {
            case "payment_intent.succeeded":
                intent = (PaymentIntent)stripeEvent.Data.Object;
                _logger.LogInformation("Payment succeded", intent.Id);
                order = await _paymentService.UpdateOrderPaymentSucceded(intent.Id);
                _logger.LogInformation("Order Updated to payment recived", order.Id);
                break;

            case "payment_intent.payment_failed":
                intent = (PaymentIntent)stripeEvent.Data.Object;
                _logger.LogInformation("Payment failed", intent.Id);
                order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                _logger.LogInformation("Order Updated to payment failed", order.Id);
                break;

        }

        return new EmptyResult();



    }

    #endregion
}