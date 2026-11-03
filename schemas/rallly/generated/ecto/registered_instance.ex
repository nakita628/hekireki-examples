defmodule Rallly.RegisteredInstance do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          instance_id: String.t(),
          version: String.t(),
          first_seen_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "registered_instances" do
    field(:id, :string, primary_key: true)
    field(:instance_id, :string)
    field(:version, :string)
    field(:first_seen_at, Rallly.PrismaDateTime, autogenerate: true)
    timestamps(inserted_at: false, updated_at_source: :last_seen_at)
  end
end