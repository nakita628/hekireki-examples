class Participant < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :name, presence: true
  validates :token, presence: true, uniqueness: true

  belongs_to :poll, inverse_of: :participants
  belongs_to :user, optional: true, inverse_of: :participants
  has_one :invite, class_name: "PollInvite", inverse_of: :participant, dependent: :nullify
  has_many :votes, dependent: :destroy
end
