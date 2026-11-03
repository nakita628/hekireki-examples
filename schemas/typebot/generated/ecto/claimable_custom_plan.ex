defmodule Typebot.ClaimableCustomPlan do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Typebot.PrismaDateTime, autogenerate: {Typebot.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          claimed_at: DateTime.t() | nil,
          name: String.t(),
          description: String.t() | nil,
          price: integer(),
          currency: String.t(),
          chats_limit: integer(),
          storage_limit: integer(),
          seats_limit: integer(),
          is_yearly: boolean(),
          company_name: String.t() | nil,
          vat_type: String.t() | nil,
          vat_value: String.t() | nil,
          inserted_at: DateTime.t(),
          workspace: Typebot.Workspace.t() | nil
        }

  schema "ClaimableCustomPlan" do
    field(:id, :string, primary_key: true)
    field(:claimed_at, Typebot.PrismaDateTime, source: :claimedAt)
    field(:name, :string)
    field(:description, :string)
    field(:price, :integer)
    field(:currency, :string)
    field(:chats_limit, :integer, source: :chatsLimit)
    field(:storage_limit, :integer, source: :storageLimit)
    field(:seats_limit, :integer, source: :seatsLimit)
    field(:is_yearly, :boolean, default: false, source: :isYearly)
    field(:company_name, :string, source: :companyName)
    field(:vat_type, :string, source: :vatType)
    field(:vat_value, :string, source: :vatValue)
    field(:workspace_id, :string, source: :workspaceId)
    belongs_to(:workspace, Typebot.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end