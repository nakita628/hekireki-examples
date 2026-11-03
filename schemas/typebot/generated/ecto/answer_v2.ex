defmodule Typebot.AnswerV2 do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Typebot.PrismaDateTime, autogenerate: {Typebot.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          block_id: String.t(),
          content: String.t(),
          attached_file_urls: map() | nil,
          inserted_at: DateTime.t(),
          result: Typebot.Result.t() | nil
        }

  schema "AnswerV2" do
    field(:block_id, :string, source: :blockId)
    field(:content, :string)
    field(:attached_file_urls, :map, source: :attachedFileUrls)
    field(:result_id, :string, source: :resultId)
    belongs_to(:result, Typebot.Result, foreign_key: :result_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end