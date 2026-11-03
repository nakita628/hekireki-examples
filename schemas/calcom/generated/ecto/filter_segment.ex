defmodule Calcom.FilterSegment do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t(),
          table_identifier: String.t(),
          scope: atom(),
          active_filters: map() | nil,
          sorting: map() | nil,
          column_visibility: map() | nil,
          column_sizing: map() | nil,
          per_page: integer(),
          search_term: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Calcom.User.t() | nil,
          team: Calcom.Team.t() | nil,
          user_preferences: [Calcom.UserFilterSegmentPreference.t()]
        }

  schema "FilterSegment" do
    field(:name, :string)
    field(:table_identifier, :string, source: :tableIdentifier)
    field(:scope, Ecto.Enum, values: [:USER, :TEAM])
    field(:active_filters, :map, source: :activeFilters)
    field(:sorting, :map)
    field(:column_visibility, :map, source: :columnVisibility)
    field(:column_sizing, :map, source: :columnSizing)
    field(:per_page, :integer, source: :perPage)
    field(:search_term, :string, source: :searchTerm)
    field(:user_id, :id, source: :userId)
    field(:team_id, :id, source: :teamId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
    belongs_to(:team, Calcom.Team, foreign_key: :team_id, define_field: false)
    has_many(:user_preferences, Calcom.UserFilterSegmentPreference, foreign_key: :segment_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end