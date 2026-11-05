defmodule Chibisafe.Bans do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Chibisafe.PrismaDateTime, autogenerate: {Chibisafe.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          ip: String.t(),
          reason: String.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "bans" do
    field(:ip, :string)
    field(:reason, :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end