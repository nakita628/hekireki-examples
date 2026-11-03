class SlackIntegration < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :project_id, uniqueness: true
  validates :team_id, presence: true
  validates :team_name, presence: true
  validates :bot_token, presence: true
  validates :bot_user_id, presence: true

  belongs_to :project
end
