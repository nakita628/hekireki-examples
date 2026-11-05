defmodule InboxZero.Payment do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          created_at: DateTime.t(),
          updated_at: DateTime.t(),
          processor_type: atom(),
          processor_id: String.t() | nil,
          processor_subscription_id: String.t() | nil,
          processor_customer_id: String.t() | nil,
          amount: integer(),
          currency: String.t(),
          status: String.t(),
          tax: integer(),
          tax_inclusive: boolean(),
          refunded: boolean(),
          refunded_at: DateTime.t() | nil,
          refunded_amount: integer() | nil,
          billing_reason: String.t() | nil,
          invoice_email_sent_at: DateTime.t() | nil,
          premium: InboxZero.Premium.t() | nil
        }

  schema "Payment" do
    field(:id, :string, primary_key: true)
    field(:created_at, InboxZero.PrismaDateTime, source: :createdAt)
    field(:updated_at, InboxZero.PrismaDateTime, source: :updatedAt)
    field(:processor_type, Ecto.Enum, values: [:LEMON_SQUEEZY, :STRIPE], default: :LEMON_SQUEEZY, source: :processorType)
    field(:processor_id, :string, source: :processorId)
    field(:processor_subscription_id, :string, source: :processorSubscriptionId)
    field(:processor_customer_id, :string, source: :processorCustomerId)
    field(:amount, :integer)
    field(:currency, :string)
    field(:status, :string)
    field(:tax, :integer)
    field(:tax_inclusive, :boolean, source: :taxInclusive)
    field(:refunded, :boolean, default: false)
    field(:refunded_at, InboxZero.PrismaDateTime, source: :refundedAt)
    field(:refunded_amount, :integer, source: :refundedAmount)
    field(:billing_reason, :string, source: :billingReason)
    field(:invoice_email_sent_at, InboxZero.PrismaDateTime, source: :invoiceEmailSentAt)
    field(:premium_id, :string, source: :premiumId)
    belongs_to(:premium, InboxZero.Premium, foreign_key: :premium_id, define_field: false, type: :string)
  end
end