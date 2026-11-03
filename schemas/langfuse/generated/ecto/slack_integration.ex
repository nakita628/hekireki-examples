defmodule Langfuse.SlackIntegration do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          team_id: String.t(),
          team_name: String.t(),
          bot_token: String.t(),
          bot_user_id: String.t(),
          installer_user_id: String.t() | nil,
          installer_slack_user_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil
        }

  schema "slack_integrations" do
    field(:id, :string, primary_key: true)
    field(:team_id, :string)
    field(:team_name, :string)
    field(:bot_token, :string)
    field(:bot_user_id, :string)
    field(:installer_user_id, :string)
    field(:installer_slack_user_id, :string)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end