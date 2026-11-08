class Challenge < ApplicationRecord
  self.table_name = "Challenge"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :difficulty, { beginner: "BEGINNER", easy: "EASY", medium: "MEDIUM", hard: "HARD", extreme: "EXTREME", event: "EVENT" },
       validate: true
  enum :status, { declined: "DECLINED", banned: "BANNED", pending: "PENDING", active: "ACTIVE" }, default: :pending,
                                                                                                  validate: true

  validates :name, presence: true, uniqueness: true
  validates :slug, presence: true, uniqueness: true
  validates :description, presence: true
  validates :shortDescription, presence: true
  validates :code, presence: true
  validates :tests, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :challenge
  has_many :vote, foreign_key: "challengeId", inverse_of: :challenge, dependent: :nullify
  has_many :submission, foreign_key: "challengeId", inverse_of: :challenge, dependent: :restrict_with_error
  has_many :comment, foreign_key: "rootChallengeId", inverse_of: :root_challenge, dependent: :nullify
  has_many :bookmark, foreign_key: "challengeId", inverse_of: :challenge, dependent: :nullify
  has_many :shared_solution, foreign_key: "challengeId", inverse_of: :challenge, dependent: :nullify
  has_many :report, foreign_key: "challengeId", inverse_of: :challenge, dependent: :nullify
  has_many :track_challenge, foreign_key: "challengeId", inverse_of: :challenge, dependent: :restrict_with_error
  has_many :notification, foreign_key: "challengeId", inverse_of: :challenge, dependent: :nullify
  has_and_belongs_to_many :tags, join_table: "_ChallengeToTag", foreign_key: "A", association_foreign_key: "B"
end
