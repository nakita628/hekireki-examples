defmodule Trigger.FeatureFlag do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          key: String.t(),
          value: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "FeatureFlag" do
    field(:id, :string, primary_key: true)
    field(:key, :string)
    field(:value, :map)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end