defmodule Papermark.DataroomQuestionAssignment do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          email: String.t() | nil,
          inserted_at: DateTime.t(),
          question: Papermark.DataroomQuestion.t() | nil,
          link: Papermark.Link.t() | nil,
          group: Papermark.ViewerGroup.t() | nil,
          viewer: Papermark.Viewer.t() | nil
        }

  schema "DataroomQuestionAssignment" do
    field(:id, :string, primary_key: true)
    field(:email, :string)
    field(:question_id, :string, source: :questionId)
    field(:link_id, :string, source: :linkId)
    field(:group_id, :string, source: :groupId)
    field(:viewer_id, :string, source: :viewerId)
    belongs_to(:question, Papermark.DataroomQuestion, foreign_key: :question_id, define_field: false, type: :string)
    belongs_to(:link, Papermark.Link, foreign_key: :link_id, define_field: false, type: :string)
    belongs_to(:group, Papermark.ViewerGroup, foreign_key: :group_id, define_field: false, type: :string)
    belongs_to(:viewer, Papermark.Viewer, foreign_key: :viewer_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end