defmodule LangWatch.SelfHostedNotice do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          severity: String.t(),
          title: String.t(),
          body: String.t(),
          url: String.t() | nil,
          fixed_in: String.t() | nil,
          status: String.t(),
          expires_at: DateTime.t() | nil,
          published_at: DateTime.t() | nil,
          published_by: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "SelfHostedNotice" do
    field(:id, :string, primary_key: true)
    field(:severity, :string)
    field(:title, :string)
    field(:body, :string)
    field(:url, :string)
    field(:fixed_in, :string, source: :fixedIn)
    field(:status, :string)
    field(:expires_at, LangWatch.PrismaDateTime, source: :expiresAt)
    field(:published_at, LangWatch.PrismaDateTime, source: :publishedAt)
    field(:published_by, :string, source: :publishedBy)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end