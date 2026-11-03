class Survey < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  enum :survey_name, { org_onboarding: "org_onboarding", user_onboarding: "user_onboarding" }, validate: true

  validates :response, exclusion: { in: [ nil ] }

  belongs_to :org, class_name: "Organization", optional: true, inverse_of: :surveys
  belongs_to :user, optional: true, inverse_of: :surveys
end
