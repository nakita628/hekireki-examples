defmodule Calcom.CreditPurchaseLog do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          credits: integer(),
          inserted_at: DateTime.t(),
          credit_balance: Calcom.CreditBalance.t() | nil
        }

  schema "CreditPurchaseLog" do
    field(:credits, :integer)
    field(:credit_balance_id, :binary_id, source: :creditBalanceId)
    belongs_to(:credit_balance, Calcom.CreditBalance, foreign_key: :credit_balance_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end