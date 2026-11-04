defmodule Papermark.PermissionGroup do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          description: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          dataroom: Papermark.Dataroom.t() | nil,
          team: Papermark.Team.t() | nil,
          links: [Papermark.Link.t()],
          access_controls: [Papermark.PermissionGroupAccessControls.t()]
        }

  schema "PermissionGroup" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:description, :string)
    field(:dataroom_id, :string, source: :dataroomId)
    field(:team_id, :string, source: :teamId)
    belongs_to(:dataroom, Papermark.Dataroom, foreign_key: :dataroom_id, define_field: false, type: :string)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    has_many(:links, Papermark.Link, foreign_key: :permission_group_id, references: :id)
    has_many(:access_controls, Papermark.PermissionGroupAccessControls, foreign_key: :group_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end