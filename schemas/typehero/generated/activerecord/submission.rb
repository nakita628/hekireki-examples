class Submission < ApplicationRecord
  self.table_name = "Submission"

  alias_attribute :created_at, :createdAt

  validates :code, presence: true
  validates :isSuccessful, inclusion: { in: [ true, false ] }

  belongs_to :user, foreign_key: "userId", inverse_of: :submission
  belongs_to :challenge, foreign_key: "challengeId", inverse_of: :submission
  has_many :vote, foreign_key: "submissionId", inverse_of: :submission, dependent: :nullify
end
