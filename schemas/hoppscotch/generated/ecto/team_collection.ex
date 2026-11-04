defmodule Hoppscotch.TeamCollection do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Hoppscotch.PrismaDateTime, autogenerate: {Hoppscotch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          title: String.t(),
          order_index: integer(),
          data: map() | nil,
          created_on: DateTime.t(),
          updated_at: DateTime.t(),
          parent: Hoppscotch.TeamCollection.t() | nil,
          team: Hoppscotch.Team.t() | nil,
          children: [Hoppscotch.TeamCollection.t()],
          requests: [Hoppscotch.TeamRequest.t()]
        }

  schema "TeamCollection" do
    field(:id, :string, primary_key: true)
    field(:title, :string)
    field(:order_index, :integer, source: :orderIndex)
    field(:data, :map)
    field(:created_on, Hoppscotch.PrismaDateTime, autogenerate: true, source: :createdOn)
    field(:parent_id, :string, source: :parentID)
    field(:team_id, :string, source: :teamID)
    belongs_to(:parent, Hoppscotch.TeamCollection, foreign_key: :parent_id, define_field: false, type: :string)
    belongs_to(:team, Hoppscotch.Team, foreign_key: :team_id, define_field: false, type: :string)
    has_many(:children, Hoppscotch.TeamCollection, foreign_key: :parent_id, references: :id)
    has_many(:requests, Hoppscotch.TeamRequest, foreign_key: :collection_id, references: :id)
    timestamps(inserted_at: false, updated_at_source: :updatedOn)
  end
end