defmodule Papermark.FeedbackResponse do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          data: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          feedback: Papermark.Feedback.t() | nil,
          view: Papermark.View.t() | nil
        }

  schema "FeedbackResponse" do
    field(:id, :string, primary_key: true)
    field(:data, :map)
    field(:feedback_id, :string, source: :feedbackId)
    field(:view_id, :string, source: :viewId)
    belongs_to(:feedback, Papermark.Feedback, foreign_key: :feedback_id, define_field: false, type: :string)
    belongs_to(:view, Papermark.View, foreign_key: :view_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end