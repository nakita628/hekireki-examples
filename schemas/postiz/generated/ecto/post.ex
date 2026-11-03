defmodule Postiz.Post do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Postiz.PrismaDateTime, autogenerate: {Postiz.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          state: atom(),
          publish_date: DateTime.t(),
          content: String.t(),
          delay: integer(),
          group: String.t(),
          batch_id: String.t() | nil,
          title: String.t() | nil,
          description: String.t() | nil,
          release_id: String.t() | nil,
          release_url: String.t() | nil,
          settings: String.t() | nil,
          image: String.t() | nil,
          approved_submit_for_order: atom(),
          creation_method: atom(),
          interval_in_days: integer() | nil,
          error: String.t() | nil,
          deleted_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          integration: Postiz.Integration.t() | nil,
          last_message: Postiz.Messages.t() | nil,
          organization: Postiz.Organization.t() | nil,
          parent_post: Postiz.Post.t() | nil,
          submitted_for_order: Postiz.Orders.t() | nil,
          submitted_for_organization: Postiz.Organization.t() | nil,
          comments: [Postiz.Comments.t()],
          errors: [Postiz.Errors.t()],
          payout_problems: [Postiz.PayoutProblems.t()],
          children_post: [Postiz.Post.t()],
          tags: [Postiz.TagsPosts.t()]
        }

  schema "Post" do
    field(:id, :string, primary_key: true)
    field(:state, Ecto.Enum, values: [:QUEUE, :PUBLISHED, :ERROR, :DRAFT], default: :QUEUE)
    field(:publish_date, Postiz.PrismaDateTime, source: :publishDate)
    field(:content, :string)
    field(:delay, :integer, default: 0)
    field(:group, :string)
    field(:batch_id, :string, source: :batchId)
    field(:title, :string)
    field(:description, :string)
    field(:release_id, :string, source: :releaseId)
    field(:release_url, :string, source: :releaseURL)
    field(:settings, :string)
    field(:image, :string)
    field(:approved_submit_for_order, Ecto.Enum, values: [:NO, :WAITING_CONFIRMATION, :YES], default: :NO, source: :approvedSubmitForOrder)
    field(:creation_method, Ecto.Enum, values: [:UNKNOWN, :WEB, :MCP, :API, :AUTOPOST, :CLI], default: :UNKNOWN, source: :creationMethod)
    field(:interval_in_days, :integer, source: :intervalInDays)
    field(:error, :string)
    field(:deleted_at, Postiz.PrismaDateTime, source: :deletedAt)
    field(:integration_id, :string, source: :integrationId)
    field(:last_message_id, :binary_id, source: :lastMessageId)
    field(:organization_id, :binary_id, source: :organizationId)
    field(:parent_post_id, :string, source: :parentPostId)
    field(:submitted_for_order_id, :binary_id, source: :submittedForOrderId)
    field(:submitted_for_organization_id, :binary_id, source: :submittedForOrganizationId)
    belongs_to(:integration, Postiz.Integration, foreign_key: :integration_id, define_field: false, type: :string)
    belongs_to(:last_message, Postiz.Messages, foreign_key: :last_message_id, define_field: false, type: :binary_id)
    belongs_to(:organization, Postiz.Organization, foreign_key: :organization_id, define_field: false, type: :binary_id)
    belongs_to(:parent_post, Postiz.Post, foreign_key: :parent_post_id, define_field: false, type: :string)
    belongs_to(:submitted_for_order, Postiz.Orders, foreign_key: :submitted_for_order_id, define_field: false, type: :binary_id)
    belongs_to(:submitted_for_organization, Postiz.Organization, foreign_key: :submitted_for_organization_id, define_field: false, type: :binary_id)
    has_many(:comments, Postiz.Comments, foreign_key: :post_id)
    has_many(:errors, Postiz.Errors, foreign_key: :post_id)
    has_many(:payout_problems, Postiz.PayoutProblems, foreign_key: :post_id)
    has_many(:children_post, Postiz.Post, foreign_key: :parent_post_id)
    has_many(:tags, Postiz.TagsPosts, foreign_key: :post_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end