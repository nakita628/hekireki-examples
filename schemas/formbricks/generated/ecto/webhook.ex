defmodule Formbricks.Webhook do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t() | nil,
          url: String.t(),
          source: atom(),
          triggers: atom(),
          survey_ids: [String.t()],
          secret: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workspace: Formbricks.Workspace.t() | nil
        }

  schema "Webhook" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:url, :string)
    field(:source, Ecto.Enum, values: [:user, :zapier, :make, :n8n, :activepieces], default: :user)
    field(:triggers, {:array, Ecto.Enum}, values: [:responseCreated, :responseUpdated, :responseFinished])
    field(:survey_ids, {:array, :string}, source: :surveyIds)
    field(:secret, :string)
    field(:workspace_id, :string, source: :workspaceId)
    belongs_to(:workspace, Formbricks.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end