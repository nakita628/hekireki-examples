defmodule Typebot.Webhook do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Typebot.PrismaDateTime, autogenerate: {Typebot.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          url: String.t() | nil,
          method: String.t(),
          query_params: map(),
          headers: map(),
          body: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          typebot: Typebot.Typebot.t() | nil
        }

  schema "Webhook" do
    field(:id, :string, primary_key: true)
    field(:url, :string)
    field(:method, :string)
    field(:query_params, :map, source: :queryParams)
    field(:headers, :map)
    field(:body, :string)
    field(:typebot_id, :string, source: :typebotId)
    belongs_to(:typebot, Typebot.Typebot, foreign_key: :typebot_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end