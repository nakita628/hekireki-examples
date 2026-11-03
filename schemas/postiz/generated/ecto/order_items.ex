defmodule Postiz.OrderItems do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          quantity: integer(),
          price: integer(),
          integration: Postiz.Integration.t() | nil,
          order: Postiz.Orders.t() | nil
        }

  schema "OrderItems" do
    field(:quantity, :integer)
    field(:price, :integer)
    field(:integration_id, :string, source: :integrationId)
    field(:order_id, :binary_id, source: :orderId)
    belongs_to(:integration, Postiz.Integration, foreign_key: :integration_id, define_field: false, type: :string)
    belongs_to(:order, Postiz.Orders, foreign_key: :order_id, define_field: false)
  end
end