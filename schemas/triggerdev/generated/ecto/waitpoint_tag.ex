defmodule Trigger.WaitpointTag do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          inserted_at: DateTime.t(),
          environment: Trigger.RuntimeEnvironment.t() | nil,
          project: Trigger.Project.t() | nil
        }

  schema "WaitpointTag" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:environment_id, :string, source: :environmentId)
    field(:project_id, :string, source: :projectId)
    belongs_to(:environment, Trigger.RuntimeEnvironment, foreign_key: :environment_id, define_field: false, type: :string)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end