defmodule Trigger.PlaygroundConversation do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          chat_id: String.t(),
          title: String.t(),
          agent_slug: String.t(),
          client_data: map() | nil,
          messages: map() | nil,
          last_event_id: String.t() | nil,
          user_id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          run: Trigger.TaskRun.t() | nil,
          project: Trigger.Project.t() | nil,
          runtime_environment: Trigger.RuntimeEnvironment.t() | nil
        }

  schema "PlaygroundConversation" do
    field(:id, :string, primary_key: true)
    field(:chat_id, :string, source: :chatId)
    field(:title, :string, default: "New conversation")
    field(:agent_slug, :string, source: :agentSlug)
    field(:client_data, :map, source: :clientData)
    field(:messages, :map)
    field(:last_event_id, :string, source: :lastEventId)
    field(:user_id, :string, source: :userId)
    field(:run_id, :string, source: :runId)
    field(:project_id, :string, source: :projectId)
    field(:runtime_environment_id, :string, source: :runtimeEnvironmentId)
    belongs_to(:run, Trigger.TaskRun, foreign_key: :run_id, define_field: false, type: :string)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:runtime_environment, Trigger.RuntimeEnvironment, foreign_key: :runtime_environment_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end