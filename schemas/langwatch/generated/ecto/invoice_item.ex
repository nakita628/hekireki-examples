defmodule LangWatch.InvoiceItem do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          description: String.t(),
          amount: float(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          invoice: LangWatch.Invoice.t() | nil
        }

  schema "InvoiceItem" do
    field(:id, :string, primary_key: true)
    field(:description, :string)
    field(:amount, :float)
    field(:invoice_id, :string, source: :invoiceId)
    belongs_to(:invoice, LangWatch.Invoice, foreign_key: :invoice_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end