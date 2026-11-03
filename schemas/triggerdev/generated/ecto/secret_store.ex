defmodule Trigger.SecretStore do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          key: String.t(),
          value: map(),
          version: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "SecretStore" do
    field(:key, :string, primary_key: true)
    field(:value, :map)
    field(:version, :string, default: "1")
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end