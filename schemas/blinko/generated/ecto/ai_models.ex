defmodule Blinko.AiModels do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Blinko.PrismaDateTime, autogenerate: {Blinko.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          title: String.t(),
          model_key: String.t(),
          capabilities: map(),
          config: map() | nil,
          sort_order: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          provider: Blinko.AiProviders.t() | nil
        }

  schema "aiModels" do
    field(:title, :string)
    field(:model_key, :string, source: :modelKey)
    field(:capabilities, :map)
    field(:config, :map)
    field(:sort_order, :integer, default: 0, source: :sortOrder)
    field(:provider_id, :id, source: :providerId)
    belongs_to(:provider, Blinko.AiProviders, foreign_key: :provider_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end