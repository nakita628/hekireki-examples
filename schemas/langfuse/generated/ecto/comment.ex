defmodule Langfuse.Comment do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          object_type: atom(),
          object_id: String.t(),
          content: String.t(),
          author_user_id: String.t() | nil,
          data_field: String.t() | nil,
          path: [String.t()],
          range_start: [integer()],
          range_end: [integer()],
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          reactions: [Langfuse.CommentReaction.t()]
        }

  schema "comments" do
    field(:id, :string, primary_key: true)
    field(:object_type, Ecto.Enum, values: [:TRACE, :OBSERVATION, :SESSION, :PROMPT])
    field(:object_id, :string)
    field(:content, :string)
    field(:author_user_id, :string)
    field(:data_field, :string)
    field(:path, {:array, :string})
    field(:range_start, {:array, :integer})
    field(:range_end, {:array, :integer})
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    has_many(:reactions, Langfuse.CommentReaction, foreign_key: :comment_id, references: :id)
    timestamps(inserted_at_source: :created_at)
  end
end