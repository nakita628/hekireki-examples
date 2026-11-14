defmodule LangWatch.Invoice do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          total_amount: float(),
          currency: String.t(),
          due_date: DateTime.t(),
          paid: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          subscription: LangWatch.Subscription.t() | nil,
          line_items: [LangWatch.InvoiceItem.t()]
        }

  schema "Invoice" do
    field(:id, :string, primary_key: true)
    field(:total_amount, :float, source: :totalAmount)
    field(:currency, :string)
    field(:due_date, LangWatch.PrismaDateTime, source: :dueDate)
    field(:paid, :boolean, default: false)
    field(:subscription_id, :string, source: :subscriptionId)
    belongs_to(:subscription, LangWatch.Subscription, foreign_key: :subscription_id, define_field: false, type: :string)
    has_many(:line_items, LangWatch.InvoiceItem, foreign_key: :invoice_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end