defmodule Plunk.Membership do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Plunk.PrismaDateTime, autogenerate: {Plunk.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          user_id: Ecto.UUID.t(),
          project_id: Ecto.UUID.t(),
          role: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Plunk.User.t() | nil,
          project: Plunk.Project.t() | nil
        }

  schema "memberships" do
    field(:role, Ecto.Enum, values: [:OWNER, :ADMIN, :MEMBER], default: :MEMBER)
    field(:user_id, :binary_id, primary_key: true, source: :userId)
    field(:project_id, :binary_id, primary_key: true, source: :projectId)
    belongs_to(:user, Plunk.User, foreign_key: :user_id, define_field: false, type: :binary_id)
    belongs_to(:project, Plunk.Project, foreign_key: :project_id, define_field: false, type: :binary_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end