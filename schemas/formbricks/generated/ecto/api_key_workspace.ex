defmodule Formbricks.ApiKeyWorkspace do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          permission: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          api_key: Formbricks.ApiKey.t() | nil,
          workspace: Formbricks.Workspace.t() | nil
        }

  schema "ApiKeyWorkspace" do
    field(:id, :string, primary_key: true)
    field(:permission, Ecto.Enum, values: [:read, :write, :manage])
    field(:api_key_id, :string, source: :apiKeyId)
    field(:workspace_id, :string, source: :workspaceId)
    belongs_to(:api_key, Formbricks.ApiKey, foreign_key: :api_key_id, define_field: false, type: :string)
    belongs_to(:workspace, Formbricks.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end