defmodule Typebot.CustomDomain do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Typebot.PrismaDateTime, autogenerate: {Typebot.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          name: String.t(),
          inserted_at: DateTime.t(),
          workspace: Typebot.Workspace.t() | nil
        }

  schema "CustomDomain" do
    field(:name, :string, primary_key: true)
    field(:workspace_id, :string, source: :workspaceId)
    belongs_to(:workspace, Typebot.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end