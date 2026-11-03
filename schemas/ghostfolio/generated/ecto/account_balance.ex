defmodule Ghostfolio.AccountBalance do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Ghostfolio.PrismaDateTime, autogenerate: {Ghostfolio.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          account_id: String.t(),
          date: DateTime.t(),
          user_id: String.t(),
          value: float(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "AccountBalance" do
    field(:account_id, :string, source: :accountId)
    field(:date, Ghostfolio.PrismaDateTime, autogenerate: true)
    field(:user_id, :string, source: :userId)
    field(:value, :float)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end