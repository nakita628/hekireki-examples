defmodule Blinko.AiProviders do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Blinko.PrismaDateTime, autogenerate: {Blinko.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          title: String.t(),
          provider: String.t(),
          base_url: String.t() | nil,
          api_key: String.t() | nil,
          config: map() | nil,
          sort_order: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          models: [Blinko.AiModels.t()]
        }

  schema "aiProviders" do
    field(:title, :string)
    field(:provider, :string)
    field(:base_url, :string, source: :baseURL)
    field(:api_key, :string, source: :apiKey)
    field(:config, :map)
    field(:sort_order, :integer, default: 0, source: :sortOrder)
    has_many(:models, Blinko.AiModels, foreign_key: :provider_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end