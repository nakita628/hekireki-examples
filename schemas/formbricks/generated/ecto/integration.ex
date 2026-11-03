defmodule Formbricks.Integration do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          type: atom(),
          config: map(),
          workspace: Formbricks.Workspace.t() | nil
        }

  schema "Integration" do
    field(:id, :string, primary_key: true)
    field(:type, Ecto.Enum, values: [:googleSheets, :notion, :airtable, :slack])
    field(:config, :map)
    field(:workspace_id, :string, source: :workspaceId)
    belongs_to(:workspace, Formbricks.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
  end
end