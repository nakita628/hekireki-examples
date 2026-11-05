defmodule InboxZero.Premium do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          pending_invites: [String.t()],
          lemon_squeezy_renews_at: DateTime.t() | nil,
          lemon_squeezy_customer_id: integer() | nil,
          lemon_squeezy_subscription_id: integer() | nil,
          lemon_squeezy_subscription_item_id: integer() | nil,
          lemon_squeezy_order_id: integer() | nil,
          lemon_squeezy_product_id: integer() | nil,
          lemon_squeezy_variant_id: integer() | nil,
          lemon_license_key: String.t() | nil,
          lemon_license_instance_id: String.t() | nil,
          lemon_subscription_status: String.t() | nil,
          stripe_customer_id: String.t() | nil,
          stripe_subscription_id: String.t() | nil,
          stripe_subscription_item_id: String.t() | nil,
          stripe_price_id: String.t() | nil,
          stripe_product_id: String.t() | nil,
          stripe_subscription_status: String.t() | nil,
          stripe_cancel_at: DateTime.t() | nil,
          stripe_cancel_at_period_end: boolean() | nil,
          stripe_renews_at: DateTime.t() | nil,
          stripe_trial_end: DateTime.t() | nil,
          stripe_trial_conversion_invoice_id: String.t() | nil,
          stripe_trial_converted_at: DateTime.t() | nil,
          stripe_cancellation_initiated_at: DateTime.t() | nil,
          stripe_canceled_at: DateTime.t() | nil,
          stripe_ended_at: DateTime.t() | nil,
          stripe_cancellation_feedback: String.t() | nil,
          stripe_cancellation_comment: String.t() | nil,
          stripe_ai_overage_last_invoice_id: String.t() | nil,
          stripe_ai_overage_last_period_end: DateTime.t() | nil,
          stripe_ai_overage_last_units: integer() | nil,
          stripe_invoice_emails_enabled: boolean(),
          apple_original_transaction_id: String.t() | nil,
          apple_latest_transaction_id: String.t() | nil,
          apple_product_id: String.t() | nil,
          apple_subscription_group_identifier: String.t() | nil,
          apple_app_account_token: String.t() | nil,
          apple_subscription_status: String.t() | nil,
          apple_environment: String.t() | nil,
          apple_purchase_date: DateTime.t() | nil,
          apple_expires_at: DateTime.t() | nil,
          apple_revoked_at: DateTime.t() | nil,
          apple_offer_discount_type: String.t() | nil,
          admin_grant_expires_at: DateTime.t() | nil,
          admin_grant_tier: atom() | nil,
          tier: atom() | nil,
          email_accounts_access: integer() | nil,
          unsubscribe_month: integer() | nil,
          unsubscribe_credits: integer() | nil,
          ai_month: integer() | nil,
          ai_credits: integer() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          users: [InboxZero.User.t()],
          admins: [InboxZero.User.t()],
          payments: [InboxZero.Payment.t()]
        }

  schema "Premium" do
    field(:id, :string, primary_key: true)
    field(:pending_invites, {:array, :string}, source: :pendingInvites)
    field(:lemon_squeezy_renews_at, InboxZero.PrismaDateTime, source: :lemonSqueezyRenewsAt)
    field(:lemon_squeezy_customer_id, :integer, source: :lemonSqueezyCustomerId)
    field(:lemon_squeezy_subscription_id, :integer, source: :lemonSqueezySubscriptionId)
    field(:lemon_squeezy_subscription_item_id, :integer, source: :lemonSqueezySubscriptionItemId)
    field(:lemon_squeezy_order_id, :integer, source: :lemonSqueezyOrderId)
    field(:lemon_squeezy_product_id, :integer, source: :lemonSqueezyProductId)
    field(:lemon_squeezy_variant_id, :integer, source: :lemonSqueezyVariantId)
    field(:lemon_license_key, :string, source: :lemonLicenseKey)
    field(:lemon_license_instance_id, :string, source: :lemonLicenseInstanceId)
    field(:lemon_subscription_status, :string, source: :lemonSubscriptionStatus)
    field(:stripe_customer_id, :string, source: :stripeCustomerId)
    field(:stripe_subscription_id, :string, source: :stripeSubscriptionId)
    field(:stripe_subscription_item_id, :string, source: :stripeSubscriptionItemId)
    field(:stripe_price_id, :string, source: :stripePriceId)
    field(:stripe_product_id, :string, source: :stripeProductId)
    field(:stripe_subscription_status, :string, source: :stripeSubscriptionStatus)
    field(:stripe_cancel_at, InboxZero.PrismaDateTime, source: :stripeCancelAt)
    field(:stripe_cancel_at_period_end, :boolean, source: :stripeCancelAtPeriodEnd)
    field(:stripe_renews_at, InboxZero.PrismaDateTime, source: :stripeRenewsAt)
    field(:stripe_trial_end, InboxZero.PrismaDateTime, source: :stripeTrialEnd)
    field(:stripe_trial_conversion_invoice_id, :string, source: :stripeTrialConversionInvoiceId)
    field(:stripe_trial_converted_at, InboxZero.PrismaDateTime, source: :stripeTrialConvertedAt)
    field(:stripe_cancellation_initiated_at, InboxZero.PrismaDateTime, source: :stripeCancellationInitiatedAt)
    field(:stripe_canceled_at, InboxZero.PrismaDateTime, source: :stripeCanceledAt)
    field(:stripe_ended_at, InboxZero.PrismaDateTime, source: :stripeEndedAt)
    field(:stripe_cancellation_feedback, :string, source: :stripeCancellationFeedback)
    field(:stripe_cancellation_comment, :string, source: :stripeCancellationComment)
    field(:stripe_ai_overage_last_invoice_id, :string, source: :stripeAiOverageLastInvoiceId)
    field(:stripe_ai_overage_last_period_end, InboxZero.PrismaDateTime, source: :stripeAiOverageLastPeriodEnd)
    field(:stripe_ai_overage_last_units, :integer, source: :stripeAiOverageLastUnits)
    field(:stripe_invoice_emails_enabled, :boolean, default: false, source: :stripeInvoiceEmailsEnabled)
    field(:apple_original_transaction_id, :string, source: :appleOriginalTransactionId)
    field(:apple_latest_transaction_id, :string, source: :appleLatestTransactionId)
    field(:apple_product_id, :string, source: :appleProductId)
    field(:apple_subscription_group_identifier, :string, source: :appleSubscriptionGroupIdentifier)
    field(:apple_app_account_token, :string, source: :appleAppAccountToken)
    field(:apple_subscription_status, :string, source: :appleSubscriptionStatus)
    field(:apple_environment, :string, source: :appleEnvironment)
    field(:apple_purchase_date, InboxZero.PrismaDateTime, source: :applePurchaseDate)
    field(:apple_expires_at, InboxZero.PrismaDateTime, source: :appleExpiresAt)
    field(:apple_revoked_at, InboxZero.PrismaDateTime, source: :appleRevokedAt)
    field(:apple_offer_discount_type, :string, source: :appleOfferDiscountType)
    field(:admin_grant_expires_at, InboxZero.PrismaDateTime, source: :adminGrantExpiresAt)
    field(:admin_grant_tier, Ecto.Enum, values: [BASIC_MONTHLY: "BASIC_MONTHLY", BASIC_ANNUALLY: "BASIC_ANNUALLY", PRO_MONTHLY: "PRO_MONTHLY", PRO_ANNUALLY: "PRO_ANNUALLY", STARTER_MONTHLY: "BUSINESS_MONTHLY", STARTER_ANNUALLY: "BUSINESS_ANNUALLY", PLUS_MONTHLY: "PLUS_MONTHLY", PLUS_ANNUALLY: "PLUS_ANNUALLY", PROFESSIONAL_MONTHLY: "BUSINESS_PLUS_MONTHLY", PROFESSIONAL_ANNUALLY: "BUSINESS_PLUS_ANNUALLY", COPILOT_MONTHLY: "COPILOT_MONTHLY", LIFETIME: "LIFETIME"], source: :adminGrantTier)
    field(:tier, Ecto.Enum, values: [BASIC_MONTHLY: "BASIC_MONTHLY", BASIC_ANNUALLY: "BASIC_ANNUALLY", PRO_MONTHLY: "PRO_MONTHLY", PRO_ANNUALLY: "PRO_ANNUALLY", STARTER_MONTHLY: "BUSINESS_MONTHLY", STARTER_ANNUALLY: "BUSINESS_ANNUALLY", PLUS_MONTHLY: "PLUS_MONTHLY", PLUS_ANNUALLY: "PLUS_ANNUALLY", PROFESSIONAL_MONTHLY: "BUSINESS_PLUS_MONTHLY", PROFESSIONAL_ANNUALLY: "BUSINESS_PLUS_ANNUALLY", COPILOT_MONTHLY: "COPILOT_MONTHLY", LIFETIME: "LIFETIME"])
    field(:email_accounts_access, :integer, source: :emailAccountsAccess)
    field(:unsubscribe_month, :integer, source: :unsubscribeMonth)
    field(:unsubscribe_credits, :integer, source: :unsubscribeCredits)
    field(:ai_month, :integer, source: :aiMonth)
    field(:ai_credits, :integer, source: :aiCredits)
    has_many(:users, InboxZero.User, foreign_key: :premium_id, references: :id)
    has_many(:admins, InboxZero.User, foreign_key: :premium_admin_id, references: :id)
    has_many(:payments, InboxZero.Payment, foreign_key: :premium_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end