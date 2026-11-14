defmodule LangWatch.PinnedTrace do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          trace_id: String.t(),
          source: atom(),
          reason: String.t() | nil,
          inserted_at: DateTime.t(),
          project: LangWatch.Project.t() | nil,
          user: LangWatch.User.t() | nil
        }

  schema "PinnedTrace" do
    field(:id, :string, primary_key: true)
    field(:trace_id, :string, source: :traceId)
    field(:source, Ecto.Enum, values: [:manual, :share], default: :manual)
    field(:reason, :string)
    field(:project_id, :string, source: :projectId)
    field(:user_id, :string, source: :userId)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:user, LangWatch.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end