defmodule Formbricks.Contact do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workspace: Formbricks.Workspace.t() | nil,
          responses: [Formbricks.Response.t()],
          attributes: [Formbricks.ContactAttribute.t()],
          displays: [Formbricks.Display.t()]
        }

  schema "Contact" do
    field(:id, :string, primary_key: true)
    field(:workspace_id, :string, source: :workspaceId)
    belongs_to(:workspace, Formbricks.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    has_many(:responses, Formbricks.Response, foreign_key: :contact_id)
    has_many(:attributes, Formbricks.ContactAttribute, foreign_key: :contact_id)
    has_many(:displays, Formbricks.Display, foreign_key: :contact_id)
    timestamps(inserted_at_source: :created_at)
  end
end