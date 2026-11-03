defmodule Papermark.VisitorGroup do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          emails: [String.t()],
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          team: Papermark.Team.t() | nil,
          links: [Papermark.LinkVisitorGroup.t()]
        }

  schema "VisitorGroup" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:emails, {:array, :string})
    field(:team_id, :string, source: :teamId)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    has_many(:links, Papermark.LinkVisitorGroup, foreign_key: :visitor_group_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end