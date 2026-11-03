defmodule Umami.Pixel do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Umami.PrismaDateTime, autogenerate: {Umami.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          slug: String.t(),
          deleted_at: DateTime.t() | nil,
          inserted_at: DateTime.t() | nil,
          updated_at: DateTime.t() | nil,
          user: Umami.User.t() | nil,
          team: Umami.Team.t() | nil
        }

  schema "pixel" do
    field(:id, :string, primary_key: true, source: :pixel_id)
    field(:name, :string)
    field(:slug, :string)
    field(:deleted_at, Umami.PrismaDateTime)
    belongs_to(:user, Umami.User, foreign_key: :user_id, type: :string)
    belongs_to(:team, Umami.Team, foreign_key: :team_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end