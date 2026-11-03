defmodule Langfuse.CommentReaction do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          emoji: String.t(),
          inserted_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          comment: Langfuse.Comment.t() | nil,
          user: Langfuse.User.t() | nil
        }

  schema "comment_reactions" do
    field(:id, :string, primary_key: true)
    field(:emoji, :string)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    belongs_to(:comment, Langfuse.Comment, foreign_key: :comment_id, type: :string)
    belongs_to(:user, Langfuse.User, foreign_key: :user_id, type: :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end