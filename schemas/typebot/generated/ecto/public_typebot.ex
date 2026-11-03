defmodule Typebot.PublicTypebot do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Typebot.PrismaDateTime, autogenerate: {Typebot.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          version: String.t() | nil,
          updated_at: DateTime.t(),
          last_activity_at: DateTime.t() | nil,
          groups: map(),
          events: map() | nil,
          variables: map(),
          edges: map(),
          theme: map(),
          settings: map(),
          inserted_at: DateTime.t(),
          typebot: Typebot.Typebot.t() | nil,
          media_ids_cache: [Typebot.RuntimeMediaIdCache.t()]
        }

  schema "PublicTypebot" do
    field(:id, :string, primary_key: true)
    field(:version, :string)
    field(:updated_at, Typebot.PrismaDateTime, autogenerate: true, source: :updatedAt)
    field(:last_activity_at, Typebot.PrismaDateTime, source: :lastActivityAt)
    field(:groups, :map)
    field(:events, :map)
    field(:variables, :map)
    field(:edges, :map)
    field(:theme, :map)
    field(:settings, :map)
    field(:typebot_id, :string, source: :typebotId)
    belongs_to(:typebot, Typebot.Typebot, foreign_key: :typebot_id, define_field: false, type: :string)
    has_many(:media_ids_cache, Typebot.RuntimeMediaIdCache, foreign_key: :public_typebot_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end