defmodule Typebot.Credentials do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Typebot.PrismaDateTime, autogenerate: {Typebot.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          data: String.t(),
          name: String.t(),
          type: String.t(),
          iv: String.t(),
          inserted_at: DateTime.t(),
          workspace: Typebot.Workspace.t() | nil
        }

  schema "Credentials" do
    field(:id, :string, primary_key: true)
    field(:data, :string)
    field(:name, :string)
    field(:type, :string)
    field(:iv, :string)
    field(:workspace_id, :string, source: :workspaceId)
    belongs_to(:workspace, Typebot.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end