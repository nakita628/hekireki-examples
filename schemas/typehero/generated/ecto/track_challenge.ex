defmodule TypeHero.TrackChallenge do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          order_id: integer(),
          challenge: TypeHero.Challenge.t() | nil,
          track: TypeHero.Track.t() | nil
        }

  schema "TrackChallenge" do
    field(:order_id, :integer, source: :orderId)
    field(:challenge_id, :id, source: :challengeId)
    field(:track_id, :id, source: :trackId)
    belongs_to(:challenge, TypeHero.Challenge, foreign_key: :challenge_id, define_field: false)
    belongs_to(:track, TypeHero.Track, foreign_key: :track_id, define_field: false)
  end
end