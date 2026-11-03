defmodule Documenso.TeamEmailVerification do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          name: String.t(),
          email: String.t(),
          token: String.t(),
          completed: boolean(),
          expires_at: DateTime.t(),
          inserted_at: DateTime.t(),
          team: Documenso.Team.t() | nil
        }

  schema "TeamEmailVerification" do
    field(:name, :string)
    field(:email, :string)
    field(:token, :string)
    field(:completed, :boolean, default: false)
    field(:expires_at, Documenso.PrismaDateTime, source: :expiresAt)
    field(:team_id, :id, source: :teamId)
    belongs_to(:team, Documenso.Team, foreign_key: :team_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end