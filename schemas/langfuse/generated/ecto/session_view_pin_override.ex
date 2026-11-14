defmodule Langfuse.SessionViewPinOverride do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          view_id: String.t() | nil,
          is_pinned: boolean(),
          position: integer(),
          project: Langfuse.Project.t() | nil,
          user: Langfuse.User.t() | nil
        }

  schema "session_view_pin_overrides" do
    field(:id, :string, primary_key: true)
    field(:view_id, :string)
    field(:is_pinned, :boolean, default: true)
    field(:position, :integer)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    belongs_to(:user, Langfuse.User, foreign_key: :user_id, type: :string)
  end
end