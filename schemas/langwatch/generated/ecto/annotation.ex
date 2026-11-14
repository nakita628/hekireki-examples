defmodule LangWatch.Annotation do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          trace_id: String.t(),
          comment: String.t() | nil,
          is_thumbs_up: boolean() | nil,
          email: String.t() | nil,
          score_options: map() | nil,
          expected_output: String.t() | nil,
          anchor_kind: String.t() | nil,
          anchor_id: String.t() | nil,
          anchor_path: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: LangWatch.Project.t() | nil,
          user: LangWatch.User.t() | nil
        }

  schema "Annotation" do
    field(:id, :string, primary_key: true)
    field(:trace_id, :string, source: :traceId)
    field(:comment, :string)
    field(:is_thumbs_up, :boolean, source: :isThumbsUp)
    field(:email, :string)
    field(:score_options, :map, source: :scoreOptions)
    field(:expected_output, :string, source: :expectedOutput)
    field(:anchor_kind, :string, source: :anchorKind)
    field(:anchor_id, :string, source: :anchorId)
    field(:anchor_path, :string, source: :anchorPath)
    field(:project_id, :string, source: :projectId)
    field(:user_id, :string, source: :userId)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:user, LangWatch.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end