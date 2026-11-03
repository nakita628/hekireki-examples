defmodule Papermark.OAuthGrantTeam do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          grant_id: String.t(),
          team_id: String.t(),
          user_id: String.t(),
          inserted_at: DateTime.t()
        }

  schema "OAuthGrantTeam" do
    field(:id, :string, primary_key: true)
    field(:grant_id, :string, source: :grantId)
    field(:team_id, :string, source: :teamId)
    field(:user_id, :string, source: :userId)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end