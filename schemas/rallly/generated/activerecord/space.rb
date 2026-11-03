class Space < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :show_branding, default: false
  attribute :hide_attribution, default: false
  attribute :shared, default: false

  enum :tier, { hobby: "hobby", pro: "pro" }, default: :hobby, validate: true
  enum :space_type, { personal: "personal", work: "work" }, validate: { allow_nil: true }

  validates :name, presence: true

  belongs_to :owner, class_name: "User", inverse_of: :spaces
  has_many :polls, dependent: :nullify
  has_many :scheduled_events, dependent: :destroy
  has_many :event_types, dependent: :destroy
  has_many :sheets, dependent: :destroy
  has_many :subscriptions, dependent: :destroy
  has_many :members, class_name: "SpaceMember", inverse_of: :space, dependent: :destroy
  has_many :member_invites, class_name: "SpaceMemberInvite", inverse_of: :space, dependent: :destroy
  has_many :api_keys, class_name: "SpaceApiKey", inverse_of: :space, dependent: :destroy
  has_many :nonprofit_applications, dependent: :destroy
  has_many :webhooks, class_name: "SpaceWebhook", inverse_of: :space, dependent: :destroy
end
