defmodule OpenSaas.Logs do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: OpenSaas.PrismaDateTime, autogenerate: {OpenSaas.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          message: String.t(),
          level: String.t(),
          inserted_at: DateTime.t()
        }

  schema "Logs" do
    field(:message, :string)
    field(:level, :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end