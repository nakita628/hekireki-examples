class Vote < ApplicationRecord
  self.table_name = "Vote"

  enum :rootType, { challenge: "CHALLENGE", comment: "COMMENT", submission: "SUBMISSION", sharedsolution: "SHAREDSOLUTION" },
       validate: true

  belongs_to :user, foreign_key: "userId", inverse_of: :vote
  belongs_to :challenge, foreign_key: "challengeId", optional: true, inverse_of: :vote
  belongs_to :comment, foreign_key: "commentId", optional: true, inverse_of: :vote
  belongs_to :submission, foreign_key: "submissionId", optional: true, inverse_of: :vote
  belongs_to :shared_solution, foreign_key: "sharedSolutionId", optional: true, inverse_of: :vote
end
