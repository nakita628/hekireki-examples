defmodule TypeHero.Track do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t(),
          slug: String.t(),
          description: String.t(),
          visible: boolean(),
          is_coming_soon: boolean(),
          track_challenges: [TypeHero.TrackChallenge.t()],
          enrolled_users: [TypeHero.User.t()]
        }

  schema "Track" do
    field(:name, :string)
    field(:slug, :string)
    field(:description, :string)
    field(:visible, :boolean, default: true)
    field(:is_coming_soon, :boolean, default: false, source: :isComingSoon)
    has_many(:track_challenges, TypeHero.TrackChallenge, foreign_key: :track_id)
    many_to_many(:enrolled_users, TypeHero.User, join_through: "_TrackToUser", join_keys: [A: :id, B: :id])
  end
end