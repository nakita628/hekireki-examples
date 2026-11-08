class TrackChallenge < ApplicationRecord
  self.table_name = "TrackChallenge"

  validates :orderId, presence: true

  belongs_to :challenge, foreign_key: "challengeId", inverse_of: :track_challenge
  belongs_to :track, foreign_key: "trackId", optional: true, inverse_of: :track_challenges
end
