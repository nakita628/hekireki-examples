defmodule Documenso.Folder do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          pinned: boolean(),
          visibility: atom(),
          type: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Documenso.User.t() | nil,
          team: Documenso.Team.t() | nil,
          parent: Documenso.Folder.t() | nil,
          envelopes: [Documenso.Envelope.t()],
          subfolders: [Documenso.Folder.t()]
        }

  schema "Folder" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:pinned, :boolean, default: false)
    field(:visibility, Ecto.Enum, values: [:EVERYONE, :MANAGER_AND_ABOVE, :ADMIN], default: :EVERYONE)
    field(:type, Ecto.Enum, values: [:DOCUMENT, :TEMPLATE])
    field(:user_id, :id, source: :userId)
    field(:team_id, :id, source: :teamId)
    field(:parent_id, :string, source: :parentId)
    belongs_to(:user, Documenso.User, foreign_key: :user_id, define_field: false)
    belongs_to(:team, Documenso.Team, foreign_key: :team_id, define_field: false)
    belongs_to(:parent, Documenso.Folder, foreign_key: :parent_id, define_field: false, type: :string)
    has_many(:envelopes, Documenso.Envelope, foreign_key: :folder_id, references: :id)
    has_many(:subfolders, Documenso.Folder, foreign_key: :parent_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end