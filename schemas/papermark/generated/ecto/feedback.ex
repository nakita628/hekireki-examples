defmodule Papermark.Feedback do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          data: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          link: Papermark.Link.t() | nil,
          responses: [Papermark.FeedbackResponse.t()]
        }

  schema "Feedback" do
    field(:id, :string, primary_key: true)
    field(:data, :map)
    field(:link_id, :string, source: :linkId)
    belongs_to(:link, Papermark.Link, foreign_key: :link_id, define_field: false, type: :string)
    has_many(:responses, Papermark.FeedbackResponse, foreign_key: :feedback_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end