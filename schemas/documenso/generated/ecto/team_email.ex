defmodule Documenso.TeamEmail do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          name: String.t(),
          email: String.t(),
          inserted_at: DateTime.t(),
          team: Documenso.Team.t() | nil
        }

  schema "TeamEmail" do
    field(:name, :string)
    field(:email, :string)
    field(:team_id, :id, source: :teamId)
    belongs_to(:team, Documenso.Team, foreign_key: :team_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end